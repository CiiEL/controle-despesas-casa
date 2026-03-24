import { useEffect, useState } from "react";
import type { FormEvent } from "react";
import { api } from "../services/api";

type Category = {
  id: number;
  description: string;
  purpose: number;
};

export function Categories() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [description, setDescription] = useState("");
  const [purpose, setPurpose] = useState("1");
  const [message, setMessage] = useState("");

  async function loadCategories() {
    try {
      const response = await api.get<Category[]>("/categories");
      setCategories(response.data);
    } catch {
      setMessage("Erro ao carregar categorias");
    }
  }

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
      await loadCategories();
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

  useEffect(() => {
    loadCategories();
  }, []);

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