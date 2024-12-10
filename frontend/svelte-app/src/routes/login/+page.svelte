<!-- src/routes/login/+page.svelte -->
 <script>
    import { prevent_default } from "svelte/internal";

    let email = ''
    let password = '';
    
    function handleLogin() {
        // Call backend to authenticate
        console.log("Login with", email, password);
    }
 </script>

 <h1>Login</h1>
 <form on:submit|preventDefault={handleLogin}>
    <label>
        Email: <input type="email" bind:value={email} />
    </label>
    <label>
        Password: <input type="password" bind:value={password} />
    </label>
    <button type="submit">Login</button>
 </form>