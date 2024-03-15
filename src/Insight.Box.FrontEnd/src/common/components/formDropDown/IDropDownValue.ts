import type {DropDownValue} from "@/common/components/formDropDown/DropDownValue";

export interface IDropDownValue<TKey, TValue> {
    toDropDownValue(): DropDownValue<TKey, TValue>;
}