import { useState } from 'react'


function Login() {
    const [loginEmail, setLoginEmail] = useState('')
    const [loginPassword, setLoginPassword] = useState('')
    const [loginErrors, setLoginErrors] = useState('')

    async function handleLogin(e) {
        e.preventDefault()

        const loginData = {
            email: loginEmail,
            password: loginPassword
        }
        const response = await fetch(
            'https://localhost:7279/api/Auth/Login',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginData)

            }
        )
        const data = await response.json()

        if (response.ok) {
            localStorage.setItem("MyToken", data.token);

            console.log(data);
        } else {
            setLoginErrors(data.message || "invalid emial or password")
            console.log(data.message)

        }
    }
    return (
        <div>
            <form onSubmit={handleLogin}>
                <input
                    type="email"
                    value={loginEmail}
                    onChange={e => setLoginEmail(e.target.value)}
                    placeholder="Email"
                />
                <br />
                <input
                    type="password"
                    value={loginPassword}
                    onChange={e => setLoginPassword(e.target.value)}
                    placeholder="Password"
                />
                <br />
                <button type={"submit"}>Login</button>

                {loginErrors && <p>{loginErrors}</p>}
            </form>
        </div>
    )
   
}
export default Login