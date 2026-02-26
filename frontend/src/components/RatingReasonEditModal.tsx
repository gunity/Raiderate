import {RatingReason} from "@/shared/ratings/types";
import React from "react";

type Props = {
    item: RatingReason,
    onClose: () => void,
    onSave: (item: RatingReason) => void,
};

export default function RatingReasonEditModal({item, onClose, onSave}: Props) {

    const [code, setCode] = React.useState<string>(item.code);
    const [value, setValue] = React.useState<number>(item.value);
    const [isActive, setIsActive] = React.useState<boolean>(item.is_active);

    return (
        <div className="w-full h-full inset-0 fixed flex items-center justify-center bg-black/40">
            <div className="rounded bg-white w-72 flex flex-col justify-between">
                <div className="flex flex-col p-3 gap-3">
                    {item.id && (
                        <label className="block text-sm font-mono w-full">
                            ID: {item.id}
                        </label>
                    )}
                    <input
                        type="text"
                        className="border rounded p-2 w-full"
                        value={code}
                        onChange={(x) => setCode(x.target.value)}
                    />

                    <input
                        type="number"
                        step="1"
                        className="border rounded p-2 w-full"
                        value={value}
                        onChange={(x) => setValue(Number(x.target.value))}
                    />

                    <label className="flex items-center justify-between gap-2 text-sm cursor-pointer">
                        <span>Active</span>
                        <input
                            type="checkbox"
                            className="h-6 w-6 cursor-pointer"
                            checked={isActive}
                            onChange={(x) => setIsActive(x.target.checked)}
                        />
                    </label>
                </div>
                <div className="flex p-3 gap-3 justify-end">
                    <button
                        className="rounded border p-2 bg-red-200 w-20 cursor-pointer"
                        onClick={onClose}
                    >
                        Cancel
                    </button>
                    <button
                        className="rounded border p-2 bg-green-200 w-20 cursor-pointer"
                        onClick={() => onSave({id: item.id, code: code, value: value, is_active: isActive})}
                    >
                        Save
                    </button>
                </div>
            </div>
        </div>
    );
}