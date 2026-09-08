const API_URL = 'https://localhost:7192/api/auth';

export const login = async(username, password) => {
    const response = await fetch(`${API_URL}/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    });

    if(!response.ok) throw new Error('Login failed');
    return await response.json();
};