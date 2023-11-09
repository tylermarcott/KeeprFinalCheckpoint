<template>


<!-- FIXME: need to find a way to hide this button or something, this button color is what's causing the stupid gray shit -->
  <button v-if="showButton" type="button" :class="`btn btn-${btnColor} invisible-button`" class="mb-2 text-light" data-bs-toggle="modal"
    :data-bs-target="`#${id}`">
    <slot name="button" class="invisible-button">
    </slot>
  </button>



  <!-- Modal Body -->
  <!-- if you want to close by clicking outside the modal, delete the last endpoint:data-bs-backdrop and data-bs-keyboard -->
  <div class="modal fade" :id="id" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
    aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <slot name="header">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </slot>
        </div>
        <div class="modal-body text-dark">
          <slot name="body">

          </slot>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: { id: { type: String, required: true }, showButton: { type: Boolean, default: true }, btnColor: { type: String, default: 'off-white' } },
  setup() {
    return {};
  },
};
</script>


<style>
.invisible-button {
  background: none !important;
  border: none !important;
  box-shadow: none !important;
  color: transparent !important;
  overflow: hidden;
  /* To make sure nothing spills out */
}
</style>
