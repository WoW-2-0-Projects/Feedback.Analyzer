export interface IMappable<TData> {

    map(data: object): TData;
}