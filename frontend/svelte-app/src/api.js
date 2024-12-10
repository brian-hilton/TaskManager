import axios from 'axios'
import { user, isAuthenticated } from './stores/user'

const API_BASE_URL = 'https://localhost:7117';

export async function login(email, password) {
    const data = await loginUser(email, password);
    user.set(data.user); // Save user data
    isAuthenticated.set(true);
    localStorage.setItem("authToken", data.token); // Save token
}

export async function loginUser(email, password) {
    const response = await fetch('${API_BASE_URL}/auth/login', {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
    });
    if (!response.ok) {
        throw new Error("Failed to log in");
    }
    return response.json();
}

export const getUserTaskLists = async (userId) => {
    try {
      console.log(`Sending request to: ${API_BASE_URL}/users/${userId}/tasklists`);
      const response = await axios.get(`${API_BASE_URL}/users/${userId}/tasklists`);
      console.log('API response:', response.data);
      return response.data;
    } catch (error) {
      console.error('API error:', error);
      throw error;
    }
  };

export const createTaskList = async (userId, taskListData) => {
    const response = await axios.post('${API_BASE_URL}/users/${userId}/tasklists', taskListData);
    return response.data;
};