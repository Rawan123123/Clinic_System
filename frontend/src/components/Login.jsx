import { useState } from 'react'
import { useNavigate } from "react-router-dom";
import { useLogin } from "../hooks/useLogin";

function Login() {
    const [loginEmail, setLoginEmail] = useState('')
    const [loginPassword, setLoginPassword] = useState('')

    const { loginErrors, loginSuccess, login } = useLogin()
    const navigate = useNavigate();

    async function handleLogin(e) {
        e.preventDefault()

        const success = await login({
            email: loginEmail,
            password: loginPassword
        })

        if (success) {
            navigate("/patients");
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
                <button type="button" onClick={() => navigate("/")}>Home</button>

                {loginSuccess && <p style={{ color: "green" }}>{loginSuccess}</p>}

                {loginErrors && <p>{loginErrors}</p>}
            </form>
        </div>
    )
}
export default Login