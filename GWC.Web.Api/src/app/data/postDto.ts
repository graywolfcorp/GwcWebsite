export class PostDto {
    public total: number;
    public skip: number;
    public limit: number;
    public items: Item[];
}

class Item {
    public fields: Fields;
}

export class Fields {
    public title: string;
    public date: string;
    public content: string;
    public tags: Tag[];
    public url: string;
    public gistId: string;
}

class Tag {
    public sys: Sys;
}

class Sys {
    public type: string;
    public linkType: string;
    public id: string;
}