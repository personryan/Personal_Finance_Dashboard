import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    console.log("Logging in with:", email, password);
    // Check if details are filled
    if (!email || !password) {
      alert("Please fill in all fields!")
      return
    }

    const userData = {
      Email: email,
      Password: password, // Ensure the backend expects `passwordHash`
    };

    // Add API call for authentication here
    try {
      const response = await fetch("http://localhost:5032/api/users/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        }, body: JSON.stringify(userData)
      });

      const data = await response.json();

      if (response.ok){
        alert("Login Successfully")
        localStorage.setItem("token", data.token);
        localStorage.setItem("refreshToken", data.refreshToken)
        // For now placeholder
        navigate("/Dashboard")
      } else {
        alert(data.message);
      }
    } catch(error){
      console.error("Login Error:", error);
      alert("Something went wrong. Please try again later.")
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="card p-4 shadow-lg" style={{ width: "350px" }}>
        <h2 className="text-center mb-4">Login</h2>

        <div className="mb-3">
          <label className="form-label">Email</label>
          <input
            type="email"
            className="form-control"
            placeholder="Enter your email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Password</label>
          <input
            type="password"
            className="form-control"
            placeholder="Enter your password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <button className="btn btn-primary w-100" onClick={handleLogin}>
          Log In
        </button>

        <div className="text-center mt-3">
          <span>Don't have an account? </span>
          <a href="/register" className="text-primary">
            Register
          </a>
        </div>
      </div>
    </div>
  );
};

export default Login;