import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { FetchWithAuth } from "../utils/FetchWithAuth";

const Dashboard = () => {
    useEffect(() =>{ const fetchTransactions = async () => {
        const response = await FetchWithAuth("http://localhost:5032/api/users/transactions", {
          method: "GET",
        });
      
        if (response.ok) {
          const data = await response.json();
          console.log("Transactions:", data);
        } else {
          console.error("Failed to fetch transactions");
        }
      };

      fetchTransactions();
    }, []);
      return (<div>Dashboard Page</div>);
}
export default Dashboard;