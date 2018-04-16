﻿(function (app) {
    app.controller('postListController', postListController);

    postListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function postListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.posts = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getPosts = getPosts;
        $scope.keyword = '';
        $scope.deletePost = deletePost;
        $scope.search = search;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        $scope.isAll = false;

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.posts, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.posts, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedPosts: JSON.stringify(listId)
                }
            }
            apiService.del('/api/post/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.$watch("posts", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function search() {
            getPosts();
        }

        function deletePost(id) {
            $ngBootbox.confirm("Bạn có chắc muốn xóa ( ID " + id + " ) ?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/post/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }
        function getPosts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('/api/post/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.posts = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load post failed.');
            });
        }


        $scope.getPosts();
    }
})(angular.module('tedushop.posts'));