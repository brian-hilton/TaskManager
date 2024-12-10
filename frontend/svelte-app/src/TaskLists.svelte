<script>
    import { onMount } from 'svelte';
    import { getUserTaskLists } from './api';
  
    let taskLists = [];
    let userId = 1; // Replace with dynamic userId later
  
    onMount(async () => {
      console.log('Fetching task lists for user:', userId);
      try {
        taskLists = await getUserTaskLists(userId);
        console.log('Fetched task lists:', taskLists);
      } catch (error) {
        console.error('Error fetching task lists:', error);
      }
    });
  </script>
  
  <h2>Task Lists</h2>
  <ul>
    {#if taskLists.length > 0}
      {#each taskLists as taskList}
        <li>{taskList.name}</li>
      {/each}
    {:else}
      <li>No task lists available</li>
    {/if}
  </ul>