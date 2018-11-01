import { Component, OnInit, Input } from '@angular/core';
import { SearchFeedResultModel } from '../shared/models/search-feed-result.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SelectCategoryComponent } from '../shared/components/select-category/select-category.component';

@Component({
  selector: 'app-search-feed-card',
  templateUrl: './search-feed-card.component.html',
  styleUrls: ['./search-feed-card.component.css']
})
export class SearchFeedCardComponent implements OnInit {

  @Input()
  public feed: SearchFeedResultModel;

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  public onSubscribeClick(): void {
    const modalRef = this.modalService.open(SelectCategoryComponent, { centered: true });
    modalRef.componentInstance.name = 'World';
  }

}
