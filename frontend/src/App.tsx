import React, { useEffect, useState } from "react";
import { api } from "./services/api";

import type { Lead, Tab } from "./types/lead";

import { LoadingIndicator } from "./components/LoadingIndicator";
import { FeedbackMessage } from "./components/FeedbackMessage";
import { Tabs } from "./components/Tabs";
import { LeadCard } from "./components/LeadCard";

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
      {loading && <LoadingIndicator />}

      {feedbackMessage && <FeedbackMessage message={feedbackMessage} />}

      <Tabs activeTab={activeTab} setActiveTab={setActiveTab} />

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
          <LeadCard
            key={lead.id}
            lead={lead}
            activeTab={activeTab}
            onAccept={handleAccept}
            onReject={handleReject}
          />
        ))}
      </div>
    </div>
  );
};

export default App;
