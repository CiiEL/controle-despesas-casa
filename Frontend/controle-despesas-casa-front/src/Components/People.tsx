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

  async function loadPeople() {
    try {
      const response = await api.get<Person[]>("/people");
      setPeople(response.data);
    } catch {
      setMessage("Erro ao carregar pessoas");
    }
  }

  async function handleCreatePerson(event: FormEvent) {
    event.preventDefault();

    try {
      await api.post("/people", {
        name,
        age: Number(age),
      });

      setMessage("Pessoa cadastrada com sucesso!");
      setName("");
      setAge("");
      await loadPeople();
    } catch {
      setMessage("Erro ao cadastrar pessoa");
    }
  }

  useEffect(() => {
    loadPeople();
  }, []);

  return (
    <div className="card">
      <h2>Pessoas</h2>

      <form className="form" onSubmit={handleCreatePerson}>
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

        <button type="submit">Cadastrar</button>
      </form>

      {message && <p className="message">{message}</p>}

      <div className="list">
        {people.map((p) => (
          <div key={p.id} className="list-item">
            <strong>{p.name}</strong>
            <span>{p.age} anos</span>
          </div>
        ))}
      </div>
    </div>
  );
}