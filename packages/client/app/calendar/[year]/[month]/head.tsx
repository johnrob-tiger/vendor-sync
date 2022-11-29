

export default async function Head({ params }: { params: { year: string, month: string }}) {

    const month = new Date(parseInt(params.year), parseInt(params.month), 1);

    const monthName = month.toLocaleString('default', { month: 'long' });

    const title = `Calendar | ${monthName} ${params.year}`;

    return (
        <>
            <title>{title}</title>
        </>
    )
}