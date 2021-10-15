export class LoadingStatus<T> {
    public loading: boolean;
    public loaded: boolean;
    public updating: boolean;
    public list: T[];
  
    constructor(data: {
      loading: boolean;
      loaded: boolean;
      updating: boolean;
      list: T[];
    }) {
      Object.assign(this, data);
    }
  }
