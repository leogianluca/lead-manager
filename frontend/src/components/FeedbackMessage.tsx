import React from "react";

interface FeedbackMessageProps {
  message: string;
}

export const FeedbackMessage: React.FC<FeedbackMessageProps> = ({ message }) => (
  <div className="fixed top-12 left-1/2 transform -translate-x-1/2 bg-orange-500 text-white px-6 py-3 rounded shadow-lg z-50 animate-fade-in-out">
    {message}
  </div>
);
