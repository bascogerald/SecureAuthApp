import {useState} from 'react';
import {login} from '../services/api';
import './Login.css'

export default function Login({ onLoginSuccess }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async(e) => {
        e.preventDefault();
        setError('');

        try {
            const data = await login(username, password);
            localStorage.setItem('jwtToken', data.token);
            onLoginSuccess();

        } catch(err) {
            setError('Invalid username or password. The bouncer says no');
        }
    };

    return(
        <main className="login-page">
            <div className="login-card">
                <section className="welcome">
                    <h1>Welcome to MDM</h1>
                    <p className="welcome-desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit, 
                        sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                        Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
                    </p>
                    <p style={{ textAlign: 'center', margin: '10px 0' }}>Don't have an account?</p>
                    <button type="button" className="btn-outline">Sign up</button>
                </section>
                <section className="login">            
                    <h2>Sign In</h2>
                    {error && <p className="error-msg">{error}</p>}
                    <form onSubmit={handleSubmit} className="login-form">
                        <label>Your Username</label>
                        <input 
                            type="text"
                            placeholder="Username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />
                        <label>Your Password</label>
                        <input 
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />
                        <button type="submit" className="btn-primary">Login</button>

                    </form>
                </section>
            </div>
        </main>
    );
}