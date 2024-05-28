// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function deleteTodo(i) {
  $.ajax({
    url: "Todo/Delete",
    type: "POST",
    data: {
      id: i,
    },
    success: function () {
      window.location.reload();
    },
  });
}

function populateForm(i) {
  $.ajax({
    url: "Todo/PopulateForm",
    type: "GET",
    data: {
      id: i,
    },
    dataType: "json",
    success: function (response) {
      $("#ItemName").val(response.itemName);
      $("#Id").val(response.id);
      $("#form-button").val("Update Todo");
      $("#form-action").attr("action", "/Todo/Update");
    },
  });
}

$("input[type='checkbox']").change(function () {
  let i = $(this).attr("id");
  $.ajax({
    url: "Todo/CompleteItem",
    type: "POST",
    data: {
      id: i,
    },
    success: function () {
      window.location.reload();
    },
  });
});
