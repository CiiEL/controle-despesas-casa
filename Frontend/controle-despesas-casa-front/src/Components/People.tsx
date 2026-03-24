import { useEffect, useState } from "react";
import type { FormEvent } from "react";
import { api } from "../services/api";

type Person = {
  id: number;
  name: string;
  age: number;
};

export function People() {
  const [people, setPeople] = useState<Person[]>([]);
  const [name, setName] = useState("");
  const [age, setAge] = useState("");
  const [message, setMessage] = useState("");
  const [editingPersonId, setEditingPersonId] = useState<number | null>(null);

  async function loadPeople() {
    try {
      const response = await api.get<Person[]>("/people");
      setPeople(response.data);
    } catch {
      setMessage("Erro ao carregar pessoas");
    }
  }

  function handleEditPerson(person: Person) {
    setEditingPersonId(person.id);
    setName(person.name);
    setAge(String(person.age));
    setMessage("");
  }

  function clearForm() {
    setEditingPersonId(null);
    setName("");
    setAge("");
  }

  async function handleDeletePerson(id: number) {
    const confirmed = window.confirm(
      "Deseja realmente excluir esta pessoa? As transações vinculadas também serão removidas."
    );

    if (!confirmed) return;

    try {
      await api.delete(`/people/${id}`);
      setMessage("Pessoa excluída com sucesso.");

      if (editingPersonId === id) {
        clearForm();
      }

      await loadPeople();
    } catch {
      setMessage("Erro ao excluir pessoa.");
    }
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();

    try {
      if (editingPersonId !== null) {
        await api.put(`/people/${editingPersonId}`, {
          name,
          age: Number(age),
        });

        setMessage("Pessoa atualizada com sucesso.");
      } else {
        await api.post("/people", {
          name,
          age: Number(age),
        });

        setMessage("Pessoa cadastrada com sucesso.");
      }

      clearForm();
      await loadPeople();
    } catch {
      setMessage("Erro ao salvar pessoa.");
    }
  }

  useEffect(() => {
    loadPeople();
  }, []);

  return (
    <div className="card">
      <h2>Pessoas</h2>

      <form className="form" onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Nome"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />

        <input
          type="number"
          placeholder="Idade"
          value={age}
          min={0}
          onChange={(e) => setAge(e.target.value)}
        />

        <button type="submit">
          {editingPersonId !== null ? "Salvar" : "Cadastrar"}
        </button>

        {editingPersonId !== null && (
          <button type="button" onClick={clearForm}>
            Cancelar
          </button>
        )}
      </form>

      {message && <p className="message">{message}</p>}

      <div className="list">
        {people.map((person) => (
          <div key={person.id} className="list-item person-item">
            <strong>{person.name}</strong>
            <span>{person.age} anos</span>

            <div className="actions">
              <button type="button" onClick={() => handleEditPerson(person)}>
                Editar
              </button>

              <button
                type="button"
                className="danger-button"
                onClick={() => handleDeletePerson(person.id)}
              >
                Excluir
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}