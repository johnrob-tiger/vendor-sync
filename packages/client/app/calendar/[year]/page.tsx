import Link from "next/link";

export default function CalendarYear({ params }: { params: { year: string }}) {

    return (<div>
        <h1>{params.year}</h1>
        <Link href={`/`}>Home</Link>
    </div>)
}