

export default async function Head({ params }: { params: { year: string }}) {

    const title = `Create Event`;
    return (
        <>
            <title>{title}</title>            
        </>
    )
}