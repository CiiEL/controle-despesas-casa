import { useState } from "react";
import type { FormEvent } from "react";
import { api } from "../services/api";
import { formatCurrency } from "../utils/Format";
import { useAppData } from "../Context/AppDataContext";

// Componente de transações: cria, lista e formata valores.
// Usa pessoas e categorias carregadas do contexto para seleção.
export function Transactions() {
  const {
    transactions,
    people,
    categories,
    loadTransactions,
    loadReport,
  } = useAppData();

  const [description, setDescription] = useState("");
  const [amount, setAmount] = useState("");
  const [type, setType] = useState("1");
  const [personId, setPersonId] = useState("");
  const [categoryId, setCategoryId] = useState("");
  const [message, setMessage] = useState("");

  // Cadastra transação na API e atualiza transações + relatório.
  // Convém validar selects antes, mas esta versão assume backend confiável.
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

      await Promise.all([
        loadTransactions(),
        loadReport(),
      ]);
    } catch (error: unknown) {
      type ApiError = {
        response?: {
          data?: {
            message?: string;
          };
        };
      };

      const messageFromApi =
        typeof error === "object" && error !== null && "response" in error
          ? (error as ApiError).response?.data?.message
          : undefined;

      setMessage(messageFromApi || "Erro ao cadastrar transação");
    }
  }

  // Traduz tipo numérico de transação para texto compreensível.
  function getTypeLabel(type: number) {
    if (type === 1) return "Despesa";
    if (type === 2) return "Receita";
    return "Não informado";
  }

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