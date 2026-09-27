import { useState } from 'react'
import './App.css'

function App() {
    const [name, setName] = useState('')
    const [loginEmail, setLoginEmail] = useState('')
    const [loginPassword, setLoginPassword] = useState('')
    const [registerEmail, setRegisterEmail] = useState('')
    const [registerPassword, setRegisterPassword] = useState('')
    const [phoneNumber, setPhoneNumber] = useState('')
    const [address, setAddress] = useState('')

    const [registerErrors, setRegisterErrors] = useState('')

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
        if (response.ok) {
            const data = await response.json()
            localStorage.setItem("MyToken", data.token);

            console.log(data);
        } else {
            const error = await response.text()
            console.log("Login failed");
            console.log(error);
        }
    }

    async function handleRegister(e) {
        e.preventDefault()

        setRegisterErrors('')

        const registerData = {
            name: name,
            email: registerEmail,
            password: registerPassword,
            phoneNumber: phoneNumber || null,
            address: address || null
        }
        const response = await fetch(
            'https://localhost:7279/api/Auth/Register',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(registerData)
            }
        )

        if (response.ok) {
            const data = await response.json()

            console.log(data);
        } else {
                const error = await response.json()

                if (error.errors?.Password) {
                    setRegisterErrors(error.errors?.Password[0])
                }
        }
    }
    return (
        < div >

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
                </form>

                <form onSubmit={handleRegister}>
                    <input
                        type="text"
                        value={name}
                        onChange={e => setName(e.target.value)}
                        placeholder="Name"
                    />
                    <br />
                    <input
                        type="email"
                        value={registerEmail}
                        onChange={e => setRegisterEmail(e.target.value)}
                        placeholder="Email"
                    />
                    <br />
                    <input
                        type="password"
                        value={registerPassword}
                        onChange={e => setRegisterPassword(e.target.value)}
                        placeholder="Password"
                />
                {registerErrors && <p>{registerErrors}</p>}
                    <br />
                    <input
                        type="tel"
                        value={phoneNumber}
                        onChange={e => setPhoneNumber(e.target.value)}
                        placeholder="Phone Number"
                    />
                    <br />
                    <input
                        type="text"
                        value={address}
                        onChange={e => setAddress(e.target.value)}
                        placeholder="Address"
                    />
                    <br />
                    <button type={"submit"}>Register</button>

                </form>

        </div>
    )
    }

    export default App  