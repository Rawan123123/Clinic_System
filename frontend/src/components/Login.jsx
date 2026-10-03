import { useState } from 'react'
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from '../config';

function Login() {
    const [loginEmail, setLoginEmail] = useState('')
    const [loginPassword, setLoginPassword] = useState('')

    const [loginErrors, setLoginErrors] = useState('')
    const [loginSuccess, setLoginSuccess] = useState('')

    const [token, setToken] = useState(localStorage.getItem("MyToken"));

    async function handleLogin(e) {
        e.preventDefault()

        setLoginErrors('')
        setLoginSuccess('')

        const loginData = {
            email: loginEmail,
            password: loginPassword
        }
        try {
            const response = await fetch(
                `${API_BASE_URL}/api/Auth/Login`,
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
                setLoginSuccess('Login is done');
                localStorage.setItem("MyToken", data.token);
                setToken(data.token);
                navigate("/patients")

                console.log(data);
            } else {
                setLoginErrors(data.message || "invalid emial or password")
                console.log(data.message)
            }
        } catch (error) {
            console.error("Error during login:", error);
            setLoginErrors("Something went wrong. Please try again.");

        }
    }
    const navigate = useNavigate();
   
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