"use client";

import {useEffect, useState} from "react";
import RatingReasonEditModal from "@/components/RatingReasonEditModal";
import {RatingReason} from "@/shared/ratings/types";
import {createRatingReason, getAllActiveRatingReasons, updateRatingReason} from "@/shared/ratings/api";

export default function AdminRatingReasonsPage() {

    const [items, setItems] = useState<RatingReason[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        (async () => {
            setLoading(true);
            setError(null);

            try {
                const items: RatingReason[] = await getAllActiveRatingReasons();
                setItems(items);
            } catch (error: unknown) {
                setError(error instanceof Error ? error.message : "error");
            } finally {
                setLoading(false);
            }
        })();
    }, []);

    const [editMode, setEditMode] = useState<boolean>(false);
    const [editValue, setEditValue] = useState<RatingReason | null>(null);

    function openModal(item: RatingReason) {
        setEditValue(item);
        setEditMode(true);
    }

    function closeModal() {
        setEditValue(null);
        setEditMode(false);
    }

    async function saveModal(item: RatingReason): Promise<void> {
        try {
            if (item.id == null) {
                const id = await createRatingReason(item);
                const created: RatingReason = {...item, id};

                setItems((prev) => [created, ...prev]);
            } else {
                await updateRatingReason(item);

                setItems((prev) => prev.map((x) => (x.id === item.id) ? item : x));
            }

            closeModal();
        } catch (error: unknown) {
            setError(error instanceof Error ? error.message : "Save failed");
        }
    }

    return (
        <div className="grid gap-3 m-2">
            {items.map((item) => (
                <div
                    key={item.id}
                    className="rounded border w-100 p-2 flex items-center justify-between"
                >
                    <div>
                        <div className="text-sm font-mono">{item.code}</div>
                        <div className="text-xs text-gray-500">id: {item.id}</div>
                    </div>

                    <div className="flex items-center gap-4">
                        <div
                            className={[
                                "text-sm font-semibold",
                                item.value > 0 ? "text-green-700" : "",
                                item.value < 0 ? "text-red-700" : "",
                                item.value === 0 ? "text-gray-700" : "",
                            ].join(" ")}
                        >
                            {item.value > 0 ? `+${item.value}` : item.value}
                        </div>

                        <span
                            className={[
                                "text-xs rounded p-2 border w-20 text-center",
                                item.is_active ? "bg-green-50 text-green-800 border-green-200" : "bg-gray-50 text-gray-700 border-gray-200",
                            ].join(" ")}
                        >
                            {item.is_active ? "active" : "disabled"}
                        </span>

                        <button
                            className="text-xs border rounded p-2 w-16 cursor-pointer"
                            onClick={() => openModal(item)}
                        >
                            Edit
                        </button>
                    </div>
                </div>
            ))}

            <button
                className="border rounded p-2 w-16 cursor-pointer"
                onClick={() => openModal({code: "", value: 0, is_active: true})}
            >
                Create
            </button>

            {editMode && editValue && (
                <RatingReasonEditModal item={editValue} onClose={closeModal} onSave={saveModal}/>
            )}
        </div>
    );
}