import React, { useEffect, useState } from "react";
import { api } from "./services/api";
import { FaHome, FaBriefcase, FaIdBadge, FaCalendarAlt } from "react-icons/fa";

export type CategoryEnum = 0 | 1 | 2 | 3;

export const CategoryLabels = {
  0: "Residential",
  1: "Comercial",
  2: "Industrial",
  3: "Other",
} as const;

type Lead = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  suburb: string;
  category: CategoryEnum;
  description: string;
  price: number;
  createdAt: string;
  status: number;
};

type Tab = "invited" | "accepted";

function stringToColor(str: string): string {
  let hash = 0;
  for (let i = 0; i < str.length; i++) {
    hash = str.charCodeAt(i) + ((hash << 5) - hash);
  }
  const color = `hsl(${hash % 360}, 70%, 50%)`;
  return color;
}

const parseDate = (dateString: string) => {
  const fixedDateString = dateString.replace(/\.(\d{3})\d+/, ".$1");
  return new Date(fixedDateString);
};

const LoadingIndicator: React.FC = () => (
  <div className="fixed top-0 left-0 w-full h-1 bg-orange-200 z-50">
    <div className="loading-bar bg-orange-500 h-1 w-20 rounded animate-loading" />
  </div>
);

const App: React.FC = () => {
  const [activeTab, setActiveTab] = useState<Tab>("invited");
  const [leads, setLeads] = useState<Lead[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [feedbackMessage, setFeedbackMessage] = useState<string | null>(null);

  useEffect(() => {
    const fetchLeads = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await api.get<Lead[]>("/Lead");
        const filteredLeads = response.data.filter(
          (lead) => lead.status === (activeTab === "invited" ? 0 : 1)
        );
        setLeads(filteredLeads);
      } catch (err: any) {
        setError(err.message || "Erro desconhecido");
      } finally {
        setLoading(false);
      }
    };

    fetchLeads();
  }, [activeTab]);

  function showFeedback(message: string) {
    setFeedbackMessage(message);
    setTimeout(() => setFeedbackMessage(null), 3000);
  }

  const handleAccept = async (id: string) => {
    try {
      await api.put(`/Lead/${id}/accept`);
      setLeads((prev) => prev.filter((lead) => lead.id !== id));
      showFeedback(`Lead ${id} aceito!`);
    } catch {
      showFeedback("Erro ao aceitar o lead.");
    }
  };

  const handleReject = async (id: string) => {
    try {
      await api.put(`/Lead/${id}/reject`);
      setLeads((prev) => prev.filter((lead) => lead.id !== id));
      showFeedback(`Lead ${id} recusado!`);
    } catch {
      showFeedback("Erro ao recusar o lead.");
    }
  };

  return (
    <div className="max-w-4xl mx-auto p-6 font-sans min-h-screen bg-gray-50 relative">
      {/* Loading indicator no topo */}
      {loading && <LoadingIndicator />}

      {/* Mensagem de feedback no topo */}
      {feedbackMessage && (
        <div className="fixed top-12 left-1/2 transform -translate-x-1/2 bg-orange-500 text-white px-6 py-3 rounded shadow-lg z-50 animate-fade-in-out">
          {feedbackMessage}
        </div>
      )}

      {/* Tabs */}
      <div className="flex justify-between mb-6 border-b-2 pb-2">
        <button
          onClick={() => setActiveTab("invited")}
          className={`relative font-semibold text-lg px-4 py-2 transition-colors duration-300 ${
            activeTab === "invited"
              ? "text-orange-600"
              : "text-gray-500 hover:text-orange-500"
          }`}
        >
          Invited
          {activeTab === "invited" && (
            <span className="absolute bottom-0 left-0 right-0 h-1 bg-orange-500 rounded-t-md transition-all duration-300"></span>
          )}
        </button>

        <button
          onClick={() => setActiveTab("accepted")}
          className={`relative font-semibold text-lg px-4 py-2 transition-colors duration-300 ${
            activeTab === "accepted"
              ? "text-orange-600"
              : "text-gray-500 hover:text-orange-500"
          }`}
        >
          Accepted
          {activeTab === "accepted" && (
            <span className="absolute bottom-0 left-0 right-0 h-1 bg-orange-500 rounded-t-md transition-all duration-300"></span>
          )}
        </button>
      </div>

      {loading && (
        <p className="text-center italic text-gray-600 select-none">
          Carregando leads...
        </p>
      )}
      {error && (
        <p className="text-center text-red-600 select-none">Erro: {error}</p>
      )}
      {!loading && leads.length === 0 && (
        <p className="text-center italic text-gray-500 select-none">
          Nenhum lead {activeTab}.
        </p>
      )}

      <div
        key={activeTab}
        className="space-y-6 transition-opacity duration-500 ease-in-out opacity-100"
      >
        {leads.map((lead) => (
          <div
            key={lead.id}
            className="border rounded-lg p-5 shadow-sm hover:shadow-lg transition-shadow bg-white"
          >
            {/* Nome e data */}
            <div className="flex flex-col items-start mb-4">
              <div className="flex items-center gap-3">
                {/* Bolinha com a primeira letra */}
                <div
                  className="w-10 h-10 rounded-full flex items-center justify-center text-white font-bold text-lg select-none"
                  style={{ backgroundColor: stringToColor(lead.firstName) }}
                >
                  {lead.firstName.charAt(0).toUpperCase()}
                </div>

                <h2 className="text-2xl font-bold text-gray-900">
                  {lead.firstName}
                </h2>

                <span className="text-sm text-gray-500 flex items-center gap-1 mt-1">
                  <FaCalendarAlt />
                  {parseDate(lead.createdAt).toLocaleString("pt-BR", {
                    day: "2-digit",
                    month: "2-digit",
                    year: "numeric",
                    hour: "2-digit",
                    minute: "2-digit",
                  })}
                </span>
              </div>
            </div>

            <hr className="mb-6" />

            {/* Linha com Endereço, Categoria e ID */}
            <div className="flex justify-between text-gray-700 font-medium mb-4">
              <p className="flex items-center gap-1">
                <FaHome />
                {lead.suburb}
              </p>
              <p className="flex items-center gap-1">
                <FaBriefcase />
                {CategoryLabels[lead.category] || "Unknown"}
              </p>
              <p className="flex items-center gap-1">
                <FaIdBadge />
                <span>Job ID:</span> {lead.id}
              </p>
            </div>

            {/* Descrição */}
            <p className="mb-6 text-gray-800">{lead.description}</p>

            {/* Botões e preço alinhados nas pontas */}
            <div className="flex justify-between items-center">
              {activeTab === "invited" ? (
                <div className="flex space-x-4">
                  <button
                    onClick={() => handleAccept(lead.id)}
                    className="bg-green-600 hover:bg-green-700 text-white px-5 py-2 rounded transition-colors duration-300 shadow flex items-center gap-2"
                  >
                    Accept
                  </button>
                  <button
                    onClick={() => handleReject(lead.id)}
                    className="bg-red-600 hover:bg-red-700 text-white px-5 py-2 rounded transition-colors duration-300 shadow flex items-center gap-2"
                  >
                    Reject
                  </button>
                </div>
              ) : (
                <div /> // placeholder vazio pra manter espaço
              )}

              <p className="text-lg font-semibold text-gray-900 flex items-center gap-1">
                {lead.price.toLocaleString("pt-BR", {
                  style: "currency",
                  currency: "BRL",
                })}
              </p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default App;
