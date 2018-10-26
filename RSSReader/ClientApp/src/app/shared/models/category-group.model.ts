import { FeedInformation } from "./feed-information.model";

export class CategoryGroup {
  name: string;
  isExpanded: boolean;
  feedsInfo: Array<FeedInformation>;

  constructor(cat: string, expanded: boolean, feeds: Array<FeedInformation>) {
    this.name = cat;
    this.isExpanded = expanded;
    this.feedsInfo = feeds;
  }
}
