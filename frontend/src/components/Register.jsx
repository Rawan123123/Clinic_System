import { useNavigate } from "react-router-dom";
import { useRegister } from "../hooks/useRegister";
import { useState } from 'react';

function Register() {
    const [name, setName] = useState('')
    const [registerEmail, setRegisterEmail] = useState('')
    const [registerPassword, setRegisterPassword] = useState('')
    const [phoneNumber, setPhoneNumber] = useState('')
    const [address, setAddress] = useState('')

    const { registerErrors, registerSuccess, register } = useRegister();
    const navigate = useNavigate();


    async function handleRegister(e) {
        e.preventDefault()

        register({
            name: name,
            email: registerEmail,
            password: registerPassword,
            phoneNumber: phoneNumber || null,
            address: address || null
        })
        
    }
    return (
        < div >

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


                {registerSuccess && 
                    <div>
                        <p style={{ color: "green" }}>{registerSuccess}</p>
                        <button type="button" onClick={() => navigate("/login")}>Login</button>
                    </div>    
                }

                {registerErrors.length > 0 &&
                    <div>
                        <ul>
                            {registerErrors.map((err, i) => <li key={i}>{err}</li>)}
                        </ul>
                        <button type="button" onClick={() => navigate("/")}>Home</button>
                    </div>
                }
            </form>
        </div>
    )
}
export default Register
