import { useState } from 'react';
import Login from './components/Login';
import './App.css';

export default function App() {
    // Check if the user already has a token in their browser
    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('jwtToken'));

    const handleLogout = () => {
        localStorage.removeItem('jwtToken'); // Rip off the wristband
        setIsAuthenticated(false); // Kick them back to login
    };

    // The Traffic Cop Logic
    if (!isAuthenticated) {
        return <Login onLoginSuccess={() => setIsAuthenticated(true)} />;
    }

    return (
        <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <h1>Welcome to the Main Page!</h1>
            <p>You have successfully authenticated using your JWT.</p>
            <button onClick={handleLogout}>Logout</button>
        </div>
    );
}