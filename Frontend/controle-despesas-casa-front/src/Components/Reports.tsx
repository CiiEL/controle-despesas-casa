import { useEffect, useState } from "react";
import { useAppData } from "../Context/AppDataContext";
import { formatCurrency } from "../utils/Format";


export function Reports() {
  const { report, loadReport } = useAppData();
  const [message, setMessage] = useState("");

  // Carrega relatório por pessoa inicial ao montar o componente.
  // A função loadReport vem do AppDataContext e mantém estado global.
  useEffect(() => {
    const fetchReport = async () => {
      try {
        await loadReport();
        setMessage("");
      } catch {
        setMessage("Erro ao carregar relatório");
      }
    };

    fetchReport();
  }, [loadReport]);

  return (
    <div className="card">
      <div className="section-header">
        <h2>Relatório por Pessoa</h2>
        <button type="button" onClick={loadReport}>
          Atualizar relatório
        </button>
      </div>

      {message && <p className="message error">{message}</p>}

      {report && (
        <>
          <div className="list">
            {report.people.map((person) => (
              <div key={person.personId} className="report-item">
                <strong>{person.name}</strong>
                <span>Receitas: {formatCurrency(person.totalIncome)}</span>
                <span>Despesas: {formatCurrency(person.totalExpense)}</span>
                <span className={person.balance < 0 ? "negative" : "positive"}>
                  Saldo: {formatCurrency(person.balance)}
                </span>
              </div>
            ))}
          </div>

          <div className="total-box">
            <h3>Total Geral</h3>
            <p>Receitas: {formatCurrency(report.totalIncome)}</p>
            <p>Despesas: {formatCurrency(report.totalExpense)}</p>
            <p className={report.balance < 0 ? "negative" : "positive"}>
              Saldo: {formatCurrency(report.balance)}
            </p>
          </div>
        </>
      )}
    </div>
  );
}