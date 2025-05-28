import React from "react";

export const LoadingIndicator: React.FC = () => (
  <div className="fixed top-0 left-0 w-full h-1 bg-orange-200 z-50">
    <div className="loading-bar bg-orange-500 h-1 w-20 rounded animate-loading" />
  </div>
);
