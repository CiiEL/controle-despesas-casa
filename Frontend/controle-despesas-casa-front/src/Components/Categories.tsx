import { useState } from "react";
import type { FormEvent } from "react";
import { api } from "../services/api";
import { useAppData } from "../Context/AppDataContext";

export function Categories() {
  const { categories, loadCategories, loadTransactions } = useAppData();

  const [description, setDescription] = useState("");
  const [purpose, setPurpose] = useState("1");
  const [message, setMessage] = useState("");

  async function handleCreateCategory(event: FormEvent) {
    event.preventDefault();

    try {
      await api.post("/categories", {
        description,
        purpose: Number(purpose),
      });

      setMessage("Categoria cadastrada com sucesso!");
      setDescription("");
      setPurpose("1");

      await Promise.all([
        loadCategories(),
        loadTransactions(),
      ]);
    } catch {
      setMessage("Erro ao cadastrar categoria");
    }
  }

  function getPurposeLabel(purpose: number) {
    if (purpose === 1) return "Despesa";
    if (purpose === 2) return "Receita";
    if (purpose === 3) return "Ambos";
    return "Não informado";
  }

  return (
    <div className="card">
      <h2>Categorias</h2>

      <form className="form" onSubmit={handleCreateCategory}>
        <input
          type="text"
          placeholder="Descrição"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <select value={purpose} onChange={(e) => setPurpose(e.target.value)}>
          <option value="1">Despesa</option>
          <option value="2">Receita</option>
          <option value="3">Ambos</option>
        </select>

        <button type="submit">Cadastrar</button>
      </form>

      {message && <p className="message">{message}</p>}

      <div className="list">
        {categories.map((category) => (
          <div key={category.id} className="list-item">
            <strong>{category.description}</strong>
            <span>{getPurposeLabel(category.purpose)}</span>
          </div>
        ))}
      </div>
    </div>
  );
}