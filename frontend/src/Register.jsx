import { useState } from 'react'


function Register() {
    const [name, setName] = useState('')

    const [registerEmail, setRegisterEmail] = useState('')
    const [registerPassword, setRegisterPassword] = useState('')
    const [phoneNumber, setPhoneNumber] = useState('')
    const [address, setAddress] = useState('')

    const [registerErrors, setRegisterErrors] = useState([])



    async function handleRegister(e) {
        e.preventDefault()

        setRegisterErrors([])

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
        const data = await response.json()

        if (response.ok) {

            console.log(data);
        } else {

            if (data.errors) {
                const allErrors = Object.values(data.errors).flat()
                setRegisterErrors(allErrors)
            }
            else {
                setRegisterErrors([data.errors || "try again"])
            }
        }
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

                {setRegisterErrors &&
                    <ul>
                        {registerErrors.map((err, i) => <li key={i}>{err}</li>)}
                    </ul>}

            </form>

        </div>
    )
}
export default Register
