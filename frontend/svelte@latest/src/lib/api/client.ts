const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost://7117';

export async function apiRequest(endpoint: string, options: RequestInit = {}) {
    const url = `${API_BASE_URL}/${endpoint}`;
    //const url = 'https://localhost:7117/getAllUsers'; // Hardcoded for testing
    const response = await fetch(url, {
        ...options,
        headers: {
            'Content-Type': 'application/json',
            ...(options.headers || {})
        },
    });
    if (!response.ok) {
        throw new Error(`API request failed: ${response.status} - ${response.statusText}`);
    }
    return response.json();
}
