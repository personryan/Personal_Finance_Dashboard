import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirm_password, setConfirmPassword] = useState("");
  
  const navigate = useNavigate();

  const handleRegister = async () => {
    console.log("Registeration with:", email, password, confirm_password);

    // Check if details are filled
    if (!email || !password || !confirm_password || !name) {
      alert("Please fill in all fields!")
      return
    }

    // Check if password matches
    if (password !== confirm_password) {
      alert("Password does not match!")
      return
    }

    const userData = {
      Name: name,
      Email: email,
      PasswordHash: password, // Ensure the backend expects `passwordHash`
    };

    // Add API call for authentication here
    try {
      const response = await fetch("http://localhost:5032/api/users/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        }, body: JSON.stringify(userData),
      });

      const data = await response.json();

      if (response.ok) {
        alert("Registration successful! Redirecting to login.");
        navigate("/login"); // Redirect to login page
      } else {
        alert(data.message || "Registration failed. Try again.");
      }
    } catch (error) {
      console.error("Registration error:", error);
      alert("Something went wrong. Please try again later.");
    }

  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="card p-4 shadow-lg" style={{ width: "350px" }}>
        <h2 className="text-center mb-4">Register</h2>

        <div className="mb-3">
          <label className="form-label">Name</label>
          <input
            type="name"
            className="form-control"
            placeholder="Enter your name"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </div>

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

        <div className="mb-3">
          <label className="form-label">Confirm Password</label>
          <input
            type="password"
            className="form-control"
            placeholder="Confirm your password"
            value={confirm_password}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </div>

        <button className="btn btn-primary w-100" onClick={handleRegister}>
          Register
        </button>

      </div>
    </div>
  );
};

export default Register;
