import React from "react";
import type { Tab } from "../types/lead";

interface TabsProps {
  activeTab: Tab;
  setActiveTab: (tab: Tab) => void;
}

export const Tabs: React.FC<TabsProps> = ({ activeTab, setActiveTab }) => (
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
);
