import "../App.jsx"

function Welcome({ message , title , name }) {

    return (
        <div>
            <p>{message}</p>
            <p>{title}</p>
            <p>{name}</p>
        </div>
    )
}

export default Welcome