export class News {
    id: string;
    title: string;
    content: string;
    isImportant: boolean;

    constructor(data: { id: string, title: string, content: string, isImportant: boolean }) {
        Object.assign(this, data);
    }
}
