const globals = {
    guidDefaultValue: '00000000-0000-0000-0000-000000000000',
    validationPattern: '^[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}$'
};

export class UUID {

    private static gen(count = 1) {
        let out = '';
        for (let i = 0; i < count; i++) out += ((1 + Math.random()) * 0x10000 | 0).toString(16).substring(1);
        return out;
    }

    private value: string;

    protected constructor(guid: string) {
        if (!guid) throw new TypeError('Invalid argument; `value` has no value.');
        this.value = globals.guidDefaultValue;
        if (guid && UUID.isGuid(guid)) this.value = guid;
    }

    static validator = new RegExp(globals.validationPattern, 'i');

    static isGuid = (guid: UUID | string): boolean => 
        (guid instanceof UUID || UUID.validator.test(guid?.toString()));

    static create = (): UUID => new UUID([UUID.gen(2), UUID.gen(), UUID.gen(), UUID.gen(), UUID.gen(3)].join('-'));

    static createEmpty = (): UUID => new UUID(globals.guidDefaultValue);

    static parse = (guid: string): UUID => new UUID(guid);

    static raw = (): string => [UUID.gen(2), UUID.gen(), UUID.gen(), UUID.gen(), UUID.gen(3)].join('-');

    static EMPTY = UUID.parse(globals.guidDefaultValue);

    /*
     * Comparing string `value` against provided `guid` will auto-call
     * toString on `guid` for comparison
     */
    equals = (other: UUID): boolean => UUID.isGuid(other) && this.value.toUpperCase() === other.toString().toUpperCase();

    isEmpty = (): boolean => this.value === globals.guidDefaultValue;

    toString = (): string => this.value;

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    toJSON(): any { return { value: this.value }; }
}
