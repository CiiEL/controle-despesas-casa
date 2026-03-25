import { useState } from "react";
import type { FormEvent } from "react";
import { api } from "../services/api";
import { useAppData } from "../Context/AppDataContext";

// Modela o cadastro de pessoas que podem ter transações associadas
// Cada pessoa tem id, nome e idade. 
// Pessoas são carregadas do backend e exibidas em lista com ações.
type Person = {
  id: number;
  name: string;
  age: number;
};

export function People() {
  // useAppData centraliza dados globais e gerencia reloads de cada entidade.
  const { people, loadPeople, loadTransactions, loadReport } = useAppData();

  const [name, setName] = useState("");
  const [age, setAge] = useState("");
  const [message, setMessage] = useState("");
  const [editingPersonId, setEditingPersonId] = useState<number | null>(null);

  // Preenche o formulário para edição da pessoa selecionada
  // e habilita estado de edição para submit feito via PUT.
  function handleEditPerson(person: Person) {
    setEditingPersonId(person.id);
    setName(person.name);
    setAge(String(person.age));
    setMessage("");
  }

  // Limpa formulário e sai do modo de edição.
  function clearForm() {
    setEditingPersonId(null);
    setName("");
    setAge("");
  }

  // Recarrega dados de pessoas, transações e relatório em paralelo
  // para manter a UI consistente após mudanças.
  async function refreshPersonRelatedData() {
    await Promise.all([
      loadPeople(),
      loadTransactions(),
      loadReport(),
    ]);
  }

  // Deleta pessoa do backend e atualiza estado global.
  // Confirmação do usuário é usada para evitar exclusões acidentais.
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

      await refreshPersonRelatedData();
    } catch {
      setMessage("Erro ao excluir pessoa.");
    }
  }

  // Salva pessoa no backend (POST para novo, PUT para existente)
  // Atualiza mensagens de sucesso/erro e recarrega dados.
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
      await refreshPersonRelatedData();
    } catch {
      setMessage("Erro ao salvar pessoa.");
    }
  }

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