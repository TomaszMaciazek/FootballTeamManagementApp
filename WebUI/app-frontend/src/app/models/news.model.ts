export class News {
    title: string;
    content: string;
    isImportant: boolean;

    constructor(data: { title: string, content: string, isImportant: boolean }) {
        Object.assign(this, data);
    }
}
