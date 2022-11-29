import Link from "next/link";

export default function AddEvent({params}: {
    params: { date: string }
}) {

    return (
        <div>
            <h1>Add Event - { params.date }</h1>
            <Link href="/calendar">Calendar</Link>
        </div>
    )
}