import React, { useState, useEffect } from "react";
import axios from "axios";

// Define the structure of the task list as per the DTO
interface TaskList {
  name: string;
  owner: string | null;
  createdDate: string | null;
  updatedDate: string | null;
}

const TaskLists: React.FC = () => {
  const [taskLists, setTaskLists] = useState<TaskList[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTaskLists = async () => {
      try {
        const userId = 1; // Example: hardcoded user ID
        const response = await axios.get<TaskList[]>(
          `https://localhost:7117/users/${userId}/tasklists`
        );
        setTaskLists(response.data);
      } catch (err) {
        setError("Failed to fetch task lists");
        console.error(err);
      }
    };

    fetchTaskLists();
  }, []); // Empty array means this effect runs only once when the component mounts

  if (error) {
    return <div>{error}</div>;
  }

  if (taskLists.length === 0) {
    return <div>No task lists found.</div>;
  }

  return (
    <div>
      <h1>Task Lists</h1>
      <ul>
        {taskLists.map((taskList, index) => (
          <li key={index}>
            <strong>{taskList.name}</strong>
            <p>Owner: {taskList.owner || "N/A"}</p>
            <p>Created: {taskList.createdDate || "Unknown"}</p>
            <p>Updated: {taskList.updatedDate || "Unknown"}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TaskLists;
