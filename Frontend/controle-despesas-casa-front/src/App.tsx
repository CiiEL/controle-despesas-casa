import { People } from "./Components/People";
import { Categories } from "./Components/Categories";
import { Transactions } from "./Components/Transactions";
import { Reports } from "./Components/Reports";
import "./App.css";

// App: compositional root component
// - agrupa os cards de Pessoas, Categorias, Transações e Relatórios
// - mantém layout de grade e cabeçalho estáticos
// - não contém estado local além da estrutura estática
function App() {
  return (
    <div className="app">
      <div className="container">
        <header className="app-header">
          <h1>Controle de Despesas</h1>
          <p>Sistema de controle de gastos residenciais</p>
        </header>

        <div className="grid">
          <People />
          <Categories />
          <Transactions />
          <Reports />
        </div>
      </div>
    </div>
  );
}

export default App;