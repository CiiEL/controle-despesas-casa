import { useEffect, useState } from "react";
import type { FormEvent } from "react";
import { api } from "../services/api";
import { formatCurrency } from "../utils/Format";
 

type Person = {
  id: number;
  name: string;
  age: number;
};

type Category = {
  id: number;
  description: string;
  purpose: number;
};

type Transaction = {
  id: number;
  description: string;
  amount: number;
  type: number;
  categoryId: number;
  categoryDescription: string;
  personId: number;
  personName: string;
};

export function Transactions() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [people, setPeople] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);

  const [description, setDescription] = useState("");
  const [amount, setAmount] = useState("");
  const [type, setType] = useState("1");
  const [personId, setPersonId] = useState("");
  const [categoryId, setCategoryId] = useState("");
  const [message, setMessage] = useState("");

  async function loadTransactions() {
    try {
      const response = await api.get<Transaction[]>("/transactions");
      setTransactions(response.data);
    } catch {
      setMessage("Erro ao carregar transações");
    }
  }

  async function loadPeople() {
    try {
      const response = await api.get<Person[]>("/people");
      setPeople(response.data);
    } catch {
      setMessage("Erro ao carregar pessoas para transação");
    }
  }

  async function loadCategories() {
    try {
      const response = await api.get<Category[]>("/categories");
      setCategories(response.data);
    } catch {
      setMessage("Erro ao carregar categorias para transação");
    }
  }

  async function handleCreateTransaction(event: FormEvent) {
    event.preventDefault();

    try {
      await api.post("/transactions", {
        description,
        amount: Number(amount),
        type: Number(type),
        personId: Number(personId),
        categoryId: Number(categoryId),
      });

      setMessage("Transação cadastrada com sucesso!");
      setDescription("");
      setAmount("");
      setType("1");
      setPersonId("");
      setCategoryId("");
      await loadTransactions();
    } catch (error: any) {
      setMessage(error?.response?.data?.message || "Erro ao cadastrar transação");
    }
  }

  function getTypeLabel(type: number) {
    if (type === 1) return "Despesa";
    if (type === 2) return "Receita";
    return "Não informado";
  }

  useEffect(() => {
    loadTransactions();
    loadPeople();
    loadCategories();
  }, []);

  return (
    <div className="card card-full">
      <h2>Transações</h2>

      <form className="form" onSubmit={handleCreateTransaction}>
        <input
          type="text"
          placeholder="Descrição"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <input
          type="number"
          placeholder="Valor"
          value={amount}
          min={0}
          step="0.01"
          onChange={(e) => setAmount(e.target.value)}
        />

        <select value={type} onChange={(e) => setType(e.target.value)}>
          <option value="1">Despesa</option>
          <option value="2">Receita</option>
        </select>

        <select value={personId} onChange={(e) => setPersonId(e.target.value)}>
          <option value="">Selecione uma pessoa</option>
          {people.map((person) => (
            <option key={person.id} value={person.id}>
              {person.name}
            </option>
          ))}
        </select>

        <select value={categoryId} onChange={(e) => setCategoryId(e.target.value)}>
          <option value="">Selecione uma categoria</option>
          {categories.map((category) => (
            <option key={category.id} value={category.id}>
              {category.description}
            </option>
          ))}
        </select>

        <button type="submit">Cadastrar</button>
      </form>

      {message && <p className="message">{message}</p>}

      <div className="list">
        {transactions.map((transaction) => (
          <div key={transaction.id} className="list-item">
            <strong>{transaction.description}</strong>
            <span>{formatCurrency(transaction.amount)}</span>
            <span>{getTypeLabel(transaction.type)}</span>
            <span>{transaction.personName}</span>
            <span>{transaction.categoryDescription}</span>
          </div>
        ))}
      </div>
    </div>
  );
}