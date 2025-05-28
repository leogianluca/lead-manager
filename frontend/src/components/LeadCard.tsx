import React from "react";
import { type Lead, CategoryLabels, type Tab } from "../types/lead";
import { FaHome, FaBriefcase, FaIdBadge, FaCalendarAlt } from "react-icons/fa";
import { stringToColor } from "../utils/colors";
import { parseDate } from "../utils/date";

interface LeadCardProps {
  lead: Lead;
  activeTab: Tab;
  onAccept: (id: string) => void;
  onReject: (id: string) => void;
}

export const LeadCard: React.FC<LeadCardProps> = ({
  lead,
  activeTab,
  onAccept,
  onReject,
}) => {
  return (
    <div className="border rounded-lg p-5 shadow-sm hover:shadow-lg transition-shadow bg-white">
      {/* Nome e data */}
      <div className="flex flex-col items-start mb-4">
        <div className="flex items-center gap-3">
          <div
            className="w-10 h-10 rounded-full flex items-center justify-center text-white font-bold text-lg select-none"
            style={{ backgroundColor: stringToColor(lead.firstName) }}
          >
            {lead.firstName.charAt(0).toUpperCase()}
          </div>

          <h2 className="text-2xl font-bold text-gray-900">{lead.firstName}</h2>

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
              onClick={() => onAccept(lead.id)}
              className="bg-green-600 hover:bg-green-700 text-white px-5 py-2 rounded transition-colors duration-300 shadow flex items-center gap-2"
            >
              Accept
            </button>
            <button
              onClick={() => onReject(lead.id)}
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
  );
};
