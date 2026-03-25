import {
  createContext,
  useContext,
  useReducer,
  useCallback,
  useEffect,
  type ReactNode,
} from "react";
import { api } from "../services/api";

// Contexto global de dados do aplicativo
// gerencia carga (listagem) e estado compartilhado de pessoas, categorias, transações e relatórios.
// Usa reducer para manter o estado previsível e acionadores (dispatch) para atualizações.

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

type PersonTotal = {
  personId: number;
  name: string;
  totalIncome: number;
  totalExpense: number;
  balance: number;
};

type PersonTotalsResponse = {
  people: PersonTotal[];
  totalIncome: number;
  totalExpense: number;
  balance: number;
};

type AppDataState = {
  people: Person[];
  categories: Category[];
  transactions: Transaction[];
  report: PersonTotalsResponse | null;
  loading: boolean;
};

type AppDataAction =
  | { type: "SET_LOADING"; payload: boolean }
  | { type: "SET_PEOPLE"; payload: Person[] }
  | { type: "SET_CATEGORIES"; payload: Category[] }
  | { type: "SET_TRANSACTIONS"; payload: Transaction[] }
  | { type: "SET_REPORT"; payload: PersonTotalsResponse | null };

const initialState: AppDataState = {
  people: [],
  categories: [],
  transactions: [],
  report: null,
  loading: false,
};

// Reducer central: sincroniza estado com ações do contexto.
// Mantém padrão Flux/Redux sem dependência externa.
function appDataReducer(state: AppDataState, action: AppDataAction): AppDataState {
  switch (action.type) {
    case "SET_LOADING":
      return { ...state, loading: action.payload };
    case "SET_PEOPLE":
      return { ...state, people: action.payload };
    case "SET_CATEGORIES":
      return { ...state, categories: action.payload };
    case "SET_TRANSACTIONS":
      return { ...state, transactions: action.payload };
    case "SET_REPORT":
      return { ...state, report: action.payload };
    default:
      return state;
  }
}

type AppDataContextType = {
  people: Person[];
  categories: Category[];
  transactions: Transaction[];
  report: PersonTotalsResponse | null;
  loading: boolean;
  loadPeople: () => Promise<void>;
  loadCategories: () => Promise<void>;
  loadTransactions: () => Promise<void>;
  loadReport: () => Promise<void>;
  refreshAll: () => Promise<void>;
};

const AppDataContext = createContext<AppDataContextType | undefined>(undefined);

export function AppDataProvider({ children }: { children: ReactNode }) {
  const [state, dispatch] = useReducer(appDataReducer, initialState);

  // Carrega lista de pessoas da API e ordena por nome (pt-BR) antes de armazenar.
  const loadPeople = useCallback(async () => {
  const response = await api.get<Person[]>("/people");

  const sortedPeople = [...response.data].sort((a, b) =>
    a.name.localeCompare(b.name, "pt-BR", { sensitivity: "base" })
  );

  dispatch({ type: "SET_PEOPLE", payload: sortedPeople });
}, []);

  // Carrega categorias e ordena por descrição.
  const loadCategories = useCallback(async () => {
  const response = await api.get<Category[]>("/categories");

  const sortedCategories = [...response.data].sort((a, b) =>
    a.description.localeCompare(b.description, "pt-BR", { sensitivity: "base" })
  );

  dispatch({ type: "SET_CATEGORIES", payload: sortedCategories });
}, []);

  // Carrega transações (sem sort adicional, depois aplica UI em componentes se necessário).
  const loadTransactions = useCallback(async () => {
    const response = await api.get<Transaction[]>("/transactions");
    dispatch({ type: "SET_TRANSACTIONS", payload: response.data });
  }, []);

  // Carrega relatório agregado por pessoa.
  const loadReport = useCallback(async () => {
    const response = await api.get<PersonTotalsResponse>("/reports/person-totals");
    dispatch({ type: "SET_REPORT", payload: response.data });
  }, []);

  // Atualiza todas as entidades em paralelo e define estado de carregamento.
  const refreshAll = useCallback(async () => {
    dispatch({ type: "SET_LOADING", payload: true });
    try {
      await Promise.all([
        loadPeople(),
        loadCategories(),
        loadTransactions(),
        loadReport(),
      ]);
    } finally {
      dispatch({ type: "SET_LOADING", payload: false });
    }
  }, [loadPeople, loadCategories, loadTransactions, loadReport]);

  // Faz refresh inicial ao montar provider.
  useEffect(() => {
    refreshAll();
  }, [refreshAll]);

  return (
    <AppDataContext.Provider
      value={{
        people: state.people,
        categories: state.categories,
        transactions: state.transactions,
        report: state.report,
        loading: state.loading,
        loadPeople,
        loadCategories,
        loadTransactions,
        loadReport,
        refreshAll,
      }}
    >
      {children}
    </AppDataContext.Provider>
  );
}

export function useAppData() {
  const context = useContext(AppDataContext);

  if (!context) {
    throw new Error("useAppData must be used within an AppDataProvider");
  }

  return context;
}