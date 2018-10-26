import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Post } from "../../models/post.model";
import { StateService } from "../../services/state.service";

@Component({
  selector: 'title-only-table',
  templateUrl: './title-only-table.component.html',
  styleUrls: ['./title-only-table.component.css']
})
export class TitleOnlyTableComponent {

  constructor(private stateService: StateService) {}

  @Input()
  public posts: Post[];

  @Input()
  public showFeed: boolean;

  @Output()
  public onPostClickEmmiter: EventEmitter<Post> = new EventEmitter<Post>();

  public onPostClick(post: Post): void {
    window.open(post.link, "_blank");
    post.isRead = true;
    this.stateService.markAsRead(post.feed.categoryTitle, post.feed.feedId);
    this.onPostClickEmmiter.emit(post);
  }
}
