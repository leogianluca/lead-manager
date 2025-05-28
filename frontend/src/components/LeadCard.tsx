import React from "react";
import { Lead } from "../types/lead";

export interface LeadCardProps {
  lead: Lead;
  onAccept?: (id: string) => void;
  onDecline?: (id: string) => void;
  IconLocation?: React.ReactNode;
  IconCategory?: React.ReactNode;
}

export const LeadCard: React.FC<LeadCardProps> = ({
  lead,
  onAccept,
  onDecline,
  IconLocation,
  IconCategory,
}) => {
  return (
    <div className="bg-white shadow-md rounded-lg p-4">
      <h2 className="text-lg font-semibold mb-2">{lead.name}</h2>
      <p className="text-sm text-gray-600 flex items-center">
        {IconLocation} {lead.city}, {lead.state}
      </p>
      <p className="text-sm text-gray-600 flex items-center">
        {IconCategory} {lead.category}
      </p>

      {onAccept && onDecline && (
        <div className="mt-4 flex space-x-2">
          <button
            onClick={() => onAccept(lead.id)}
            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
          >
            Aceitar
          </button>
          <button
            onClick={() => onDecline(lead.id)}
            className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600"
          >
            Recusar
          </button>
        </div>
      )}
    </div>
  );
};
