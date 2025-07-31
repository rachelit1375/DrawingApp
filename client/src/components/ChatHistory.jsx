import React from "react";
import "../css/ChatHistory.css";

function ChatHistory({ messages }) {
  return (
    <div className="chat-history">
      {messages.map((msg, index) => (
        <div
          key={index}
          className={`chat-message ${msg.type === "user" ? "user-msg" : "bot-msg"}`}
        >
          {msg.text}
        </div>
      ))}
    </div>
  );
}

export default ChatHistory;
