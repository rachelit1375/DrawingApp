import React, { useState } from "react";
import LoginForm from "./pages/LoginForm";
import RegisterForm from "./pages/RegisterForm";
import DrawingPage from "./pages/DrawingPage";
import "./App.css";
import "./css/Users.css";

function App() {
  const [showLogin, setShowLogin] = useState(true);
  const [user, setUser] = useState(null);

  if (!user) {
    return (
      <div className="auth-container">
        <h1>🎨 אפליקציית ציור</h1>
        {showLogin ? (
          <LoginForm onLogin={setUser} onSwitch={() => setShowLogin(false)} />
        ) : (
          <RegisterForm onRegister={setUser} onSwitch={() => setShowLogin(true)} />
        )}
      </div>
    );
  }

  return <DrawingPage user={user} />;
}

export default App;