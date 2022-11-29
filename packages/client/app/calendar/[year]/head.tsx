

export default async function Head({ params }: { params: { year: string }}) {

    const title = `Calendar | ${params.year}`;
    return (
        <>
            <title>{title}</title>            
        </>
    )
}