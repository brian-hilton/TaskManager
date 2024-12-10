<script>
    import { onMount } from 'svelte';
    import { getUserTaskLists } from '../../api';

    let taskLists = [];
    onMount(async () => {
        try {
            taskLists = await getUserTaskLists(1); // Replace with logged-in user id
        } catch (error) {
            console.error("Failed to fetch tasklists:", error)
        }
    });
</script>

<h1 class="text-2xl font-bold">Tasklists
    <ul>
        {#each taskLists as tasklist}
        <li>{tasklist.name}</li>
        {/each}
    </ul>
</h1>

<nav>
    <a href="/">Home</a>
    <a href="/">Dashboard</a>
    <a href="/profile">Profile</a>
</nav>