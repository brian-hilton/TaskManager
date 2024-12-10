<script lang="ts">
    import { onMount } from 'svelte'
    import { apiRequest } from '$lib/api/client';

    let data: any = null;
    let error: string | null = null;

    onMount(async () => {
        try {
            data = await apiRequest('getAllUsers'); // apiRequest prepends localhost7117; we pass in the specific route only
        } catch (err) {
            error = err.message
        }
    });
</script>

<main>
    <h1>Testing User Call to Containerized SQL Server</h1>
    {#if error}
        <p style="color: red;">Error: {error}</p>
    {:else if data}
        <pre>{JSON.stringify(data, null, 2)}</pre>
    {:else}
        <p>Loading....</p>
    {/if}
</main>